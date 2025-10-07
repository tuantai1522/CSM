import { date, integer, pgTable, varchar } from 'drizzle-orm/pg-core';
import { addressesTable } from './address.schema';
import { createdAt, updatedAt } from './baseFields';

export const usersTable = pgTable('users', {
  id: integer('id').primaryKey().generatedAlwaysAsIdentity({ startWith: 1000 }),
  firstName: varchar('first_name', { length: 256 }).notNull(),
  middleName: varchar('middle_name', { length: 256 }),
  lastName: varchar('last_name', { length: 256 }),
  email: varchar('email', { length: 256 }).notNull().unique(),
  password: varchar('password', { length: 1024 }).notNull(),
  dateOfBirth: date('date_of_birth'),

  addressId: integer('address_id').references(() => addressesTable.id, {
    onDelete: 'set null',
  }),

  ...createdAt,
  ...updatedAt,
});
