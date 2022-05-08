import enum
from flask import Flask, request, make_response, jsonify
from flask_sqlalchemy import SQLAlchemy
from flask_marshmallow import Marshmallow
from datetime import datetime

app = Flask(__name__)
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///random.db'
db = SQLAlchemy(app)

ma = Marshmallow(app)

class GenderEnum(enum.Enum):
    Female = 'Female'
    Male = 'Male'
    NonBinary = 'Non-binary'


class RandomData(db.Model):
    id = db.Column(db.Integer, primary_key=True)
    first_name = db.Column(db.String(150), nullable=False)
    last_name = db.Column(db.String(300), nullable=False)
    email = db.Column(db.String(500), nullable=False)
    gender = db.Column(db.String(9), nullable=False)
    ip_address = db.Column(db.String(30), nullable=False)
    date_created = db.Column(db.String, nullable=True)

    def __init__(self, first_name, last_name, email, gender, ip_address):
        self.first_name = first_name
        self.last_name = last_name
        self.email = email
        self.gender = gender
        self.ip_address = ip_address


class RandomDataSchema(ma.Schema):
    class Meta:
        fields = ('id', 'first_name', 'last_name', 'email', 'gender', 'ip_address', 'date_created')
        model = RandomData
        sqla_session = db.session


random_data_schema = RandomDataSchema()
random_datas_schema = RandomDataSchema(many=True)

@app.route('/random/data/<int:amount>', methods=['GET'])
def hello_world(amount):
    data = RandomData.query.order_by(RandomData.id).limit(amount).all()
    return random_datas_schema.jsonify(data)

if __name__ == '__main__':
    app.run(debug=True, host='localhost', port=9000)
