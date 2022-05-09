const jwt = require('jsonwebtoken');

const randomInt = (min, max) => Math.floor(min + Math.random() * (max - min));

const csvFilter = (req, file, cb) => {
  if (file.mimetype === 'text/csv') {
    cb(null, true);
  } else {
    cb(null, false);
  }
};

const verifyToken = (req, res, next) => {
  const token = req.headers.authorization.split(' ')[1];

  if (!token) {
    return res.status(403).send('Nie wprowadzono tokenu autoryzacyjnego');
  }

  jwt.verify(token, global.TOKEN_SECRET, (err) => {
    if (err) {
      return res.status(401).send('Nie udało się zweryfikować tokenu');
    }
    next();
  });
};

module.exports = {
  randomInt,
  csvFilter,
  verifyToken,
};
