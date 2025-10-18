import { CountryEntity } from './country.entity';

export interface ICountryRepository {
  create(data: CountryEntity): Promise<CountryEntity>;
  getCountries(): Promise<CountryEntity[]>;
  getById(id: number): Promise<CountryEntity | null>;
}
