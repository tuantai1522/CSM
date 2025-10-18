import { Injectable } from '@nestjs/common';
import { ICountryRepository } from '../../core/country.repository.interface';
import { CountryEntity } from '../../core/country.entity';
import { countriesTable } from 'src/database/drizzle/schema';
import { DrizzleService } from 'src/database/drizzle/drizzle.service';
import { eq } from 'drizzle-orm';
import { CountryMapper } from '../../useCase/mappers/country.mapper';

@Injectable()
export class CountryRepository implements ICountryRepository {
  constructor(
    private readonly drizzle: DrizzleService,
    private readonly mapper: CountryMapper,
  ) {}
  async create(data: CountryEntity): Promise<CountryEntity> {
    const [row] = await this.drizzle.db
      .insert(countriesTable)
      .values(this.mapper.toPersistence(data))
      .returning();

    return this.mapper.toDomain(row);
  }

  async getById(id: number): Promise<CountryEntity | null> {
    const row = await this.drizzle.db.query.countriesTable.findFirst({
      columns: { id: true, name: true },
      where: eq(countriesTable.id, id),
    });

    return row ? this.mapper.toDomain(row) : null;
  }
  async getCountries(): Promise<CountryEntity[]> {
    const result = await this.drizzle.db.select().from(countriesTable);

    return result.map((r) => this.mapper.toDomain(r));
  }
}
