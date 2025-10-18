import { Inject, Injectable } from '@nestjs/common';
import { COUNTRY_REPOSITORY } from '../../../infrastructure/country.di-tokens';
import type { ICountryRepository } from '../../../core/country.repository.interface';
import { CountryEntity } from '../../../core/country.entity';
import { CountryMapper } from '../../mappers/country.mapper';
import { CountryResponseDto } from '../../dtos/country.response.dto';

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
