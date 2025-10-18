import { Module, Provider } from '@nestjs/common';
import { CountryController } from './web/country.controller';
import { COUNTRY_REPOSITORY } from './infrastructure/country.di-tokens';
import { CountryRepository } from './infrastructure/repositories/country.repository';
import { CreateCountryUseCase } from './useCase/features/createCountry/create-country.usecase';
import { DrizzleModule } from 'src/database/drizzle.module';
import { GetCountriesUseCase } from './useCase/features/getCountries/get-countries.usecase';
import { GetCountryByIdUsecase } from './useCase/features/getCountryById/get-country-by-id.usecase';
import { CountryMapper } from './useCase/mappers/country.mapper';

const commandHandlers: Provider[] = [CreateCountryUseCase];
const queryHandlers: Provider[] = [GetCountriesUseCase, GetCountryByIdUsecase];

const repositories: Provider[] = [
  { provide: COUNTRY_REPOSITORY, useClass: CountryRepository },
];

const mappers: Provider[] = [CountryMapper];

@Module({
  imports: [DrizzleModule],
  controllers: [CountryController],
  providers: [
    ...commandHandlers,
    ...queryHandlers,
    ...repositories,
    ...mappers,
  ],
})
export class CountryModule {}
