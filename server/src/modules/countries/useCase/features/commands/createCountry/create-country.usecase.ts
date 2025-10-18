import { Inject, Injectable } from '@nestjs/common';
import { CountryEntity } from 'src/modules/countries/core/country.entity';
import type { ICountryRepository } from 'src/modules/countries/core/country.repository.interface';
import { COUNTRY_REPOSITORY } from 'src/modules/countries/infrastructure/country.di-tokens';
import { CreateCountryRequest } from './create-country.dto';

@Injectable()
export class CreateCountryUseCase {
  constructor(
    @Inject(COUNTRY_REPOSITORY)
    private readonly countryRepository: ICountryRepository,
  ) {}

  async execute(request: CreateCountryRequest): Promise<CountryEntity> {
    return await this.countryRepository.create(
      CountryEntity.create(request.name),
    );
  }
}
