import { Module, Provider } from '@nestjs/common';
import { CountryController } from './web/country.controller';
import { COUNTRY_REPOSITORY } from './infrastructure/country.di-tokens';
import { CountryRepository } from './infrastructure/repositories/country.repository';
import { DrizzleModule } from 'src/database/drizzle.module';
import { CountryMapper } from './useCase/mappers/country.mapper';
import { CreateCountryUseCase } from './useCase/features/commands/createCountry/create-country.usecase';
import { GetCountriesUseCase } from './useCase/features/queries/getCountries/get-countries.usecase';
import { GetCountryByIdUsecase } from './useCase/features/queries/getCountryById/get-country-by-id.usecase';

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
