import { Inject, Injectable } from '@nestjs/common';
import { COUNTRY_REPOSITORY } from '../../../infrastructure/country.di-tokens';
import type { ICountryRepository } from '../../../core/country.repository.interface';
import { CreateCountryRequest } from './create-country.dto';
import { CountryEntity } from '../../../core/country.entity';

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
