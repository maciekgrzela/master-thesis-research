import { DocumentBuilder, SwaggerModule } from '@nestjs/swagger';

import { MainModule } from './main.module';
import { NestExpressApplication } from '@nestjs/platform-express';
import { NestFactory } from '@nestjs/core';
import { config as dotenvConfig } from 'dotenv';

const bootstrappingFunction = async () => {
  dotenvConfig();

  const app = await NestFactory.create<NestExpressApplication>(MainModule, {
    abortOnError: false,
    bodyParser: true,
    cors: false,
  });

  const config = new DocumentBuilder()
    .setTitle(process.env.SWAGGER_TITLE)
    .setDescription(process.env.SWAGGER_DESC)
    .setVersion(process.env.VERSION)
    .build();

  const document = SwaggerModule.createDocument(app, config);
  SwaggerModule.setup('api', app, document);

  await app.listen(process.env.PORT);

  console.log(`Server started at http://localhost:${process.env.PORT}`);
};

bootstrappingFunction();
