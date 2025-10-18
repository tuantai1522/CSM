import { Inject, Injectable } from '@nestjs/common';
import type { ICountryRepository } from 'src/modules/countries/core/country.repository.interface';
import { COUNTRY_REPOSITORY } from 'src/modules/countries/infrastructure/country.di-tokens';
import { CountryResponseDto } from '../../../dtos/country.response.dto';
import { CountryMapper } from '../../../mappers/country.mapper';

@Injectable()
export class GetCountryByIdUsecase {
  constructor(
    @Inject(COUNTRY_REPOSITORY)
    private readonly countryRepository: ICountryRepository,
    private readonly countryMapper: CountryMapper,
  ) {}

  async execute(id: number): Promise<CountryResponseDto | null> {
    const country = await this.countryRepository.getById(id);

    const response = this.countryMapper.toResponse(country!);
    return response;
  }
}
