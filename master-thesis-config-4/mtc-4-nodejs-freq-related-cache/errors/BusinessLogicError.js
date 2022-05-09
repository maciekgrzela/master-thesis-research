class BusinessLogicError extends Error {
  constructor(statusCode, content) {
    super(content);
    this.statusCode = statusCode;
  }
}

module.exports = BusinessLogicError;
