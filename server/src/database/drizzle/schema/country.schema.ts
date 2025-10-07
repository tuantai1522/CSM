import { integer, pgTable, varchar } from 'drizzle-orm/pg-core';

export const countriesTable = pgTable('countries', {
  id: integer('id').primaryKey().generatedAlwaysAsIdentity(),
  name: varchar('name', { length: 256 }).notNull().unique(),
});
