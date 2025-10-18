import { Inject, Injectable } from '@nestjs/common';
import { COUNTRY_REPOSITORY } from '../../../infrastructure/country.di-tokens';
import type { ICountryRepository } from '../../../core/country.repository.interface';
import { CountryResponseDto } from '../../dtos/country.response.dto';
import { CountryMapper } from '../../mappers/country.mapper';

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
