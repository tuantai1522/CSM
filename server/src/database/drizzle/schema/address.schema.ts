import { integer, pgTable, varchar } from 'drizzle-orm/pg-core';
import { citiesTable } from './city.schema';

export const addressesTable = pgTable('addresses', {
  id: integer('id').primaryKey().generatedAlwaysAsIdentity(),
  street: varchar('street', { length: 512 }),
  cityId: integer('city_id').references(() => citiesTable.id, {
    onDelete: 'set null',
  }),
});
