const redis = require('redis');
const executionAndInvalidationInfo = require('./executionAndInvalidation');
const client = redis.createClient(6379);

const staticCacheMiddleware = async (req, res, next) => {
  const requestBaseUrl = req.baseUrl;
  const requestOriginalUrl = req.originalUrl;
  const requestQueryParams = req.query;
  const requestParams = req.params;

  const info = executionAndInvalidationInfo.findIndex(
    (p) => p.endpointName === requestOriginalUrl
  );

  if (info !== -1) {
    executionAndInvalidationInfo[info].executionCount += 1;
  } else {
    executionAndInvalidationInfo.push({
      endpointName: requestOriginalUrl,
      executionCount: 1,
      invalidationCount: 0,
    });
  }

  const cacheKey = getCacheKey(
    requestBaseUrl,
    requestQueryParams,
    requestParams
  );

  res.locals.cacheKey = cacheKey;

  if (!client.isOpen) {
    await client.connect();
  }

  client
    .get(cacheKey)
    .then(async (err, cachedResponse) => {
      if (cachedResponse) {
        return res.status(200).send({
          error: false,
          data: JSON.parse(cachedResponse),
        });
      }

      next();
    })
    .catch((err) => console.log(err));
};

const cacheResponseMiddleware = async (req, res, next) => {
  const content = res.locals.content;
  const status = res.locals.status;
  const cacheKey = res.locals.cacheKey;

  if (status === 200) {
    if (!client.isOpen) {
      await client.connect();
    }
    client.setEx(cacheKey, 600, JSON.stringify(content));
  }

  return res.status(status).send({
    error: status !== 200,
    data: content,
  });
};

const invalidateCacheMiddleware = async (req, res, next) => {
  const entity = res.locals.entity;
  const content = res.locals.content;
  const status = res.locals.status;

  if (!client.isOpen) {
    await client.connect();
  }

  const info = executionAndInvalidationInfo.filter((p) =>
    p.endpointName.includes(entity)
  );

  info.forEach((item) => {
    item.invalidationCount += 1;
  });

  const keys = await client.keys(`*${entity}*`);

  await client.del(keys);

  return res.status(status).send({
    error: status !== 201 && status !== 200 && status !== 204,
    data: content,
  });
};

const getCacheKey = (baseUrl, queryParams, requestParams) => {
  let cacheKey = baseUrl;

  Object.entries(queryParams).forEach(([key, value]) => {
    cacheKey += `;${key}|${value}`;
  });

  Object.entries(requestParams).forEach(([key, value]) => {
    cacheKey += `;${key}|${value}`;
  });

  return cacheKey;
};

module.exports = {
  staticCacheMiddleware,
  cacheResponseMiddleware,
  invalidateCacheMiddleware,
};
