import { Inject, Injectable } from '@nestjs/common';
import { COUNTRY_REPOSITORY } from 'src/modules/countries/infrastructure/country.di-tokens';
import { CountryResponseDto } from '../../../dtos/country.response.dto';
import { CountryMapper } from '../../../mappers/country.mapper';
import type { ICountryRepository } from '../../../../core/country.repository.interface';

@Injectable()
export class GetCountriesUseCase {
  constructor(
    @Inject(COUNTRY_REPOSITORY)
    private readonly countryRepository: ICountryRepository,
    private readonly countryMapper: CountryMapper,
  ) {}

  async execute(): Promise<CountryResponseDto[]> {
    const countries = await this.countryRepository.getCountries();

    const response = countries.map((country) =>
      this.countryMapper.toResponse(country),
    );
    return response;
  }
}
