import { integer, pgTable, varchar } from 'drizzle-orm/pg-core';
import { countriesTable } from './country.schema';

export const citiesTable = pgTable('cities', {
  id: integer('id').primaryKey().generatedAlwaysAsIdentity(),
  name: varchar('name', { length: 256 }).notNull().unique(),
  countryId: integer('country_id')
    .notNull()
    .references(() => countriesTable.id, { onDelete: 'cascade' }),
});
