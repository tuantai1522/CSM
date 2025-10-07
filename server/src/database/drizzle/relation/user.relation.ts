import { relations } from 'drizzle-orm';
import { addressesTable } from '../schema/address.schema';
import { usersTable } from '../schema';

export const usersRelations = relations(usersTable, ({ one }) => ({
  country: one(addressesTable, {
    fields: [usersTable.addressId],
    references: [addressesTable.id],
  }),
}));
