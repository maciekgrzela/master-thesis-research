class DataAccessError extends Error {
  constructor(statusCode, content) {
    super(content);
    this.statusCode = statusCode;
  }
}

module.exports = DataAccessError;
