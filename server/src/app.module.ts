import { Module } from '@nestjs/common';
import { DrizzleModule } from './database/drizzle.module';
import { CountryModule } from './modules/countries/country.module';
import { ConfigModule } from '@nestjs/config';

@Module({
imports: [
    ConfigModule.forRoot({
      isGlobal: true,
      expandVariables: true,
    }),
    DrizzleModule,
    CountryModule,
  ],
})
export class AppModule {}
