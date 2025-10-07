import { relations } from 'drizzle-orm';
import { citiesTable } from '../schema/city.schema';
import { addressesTable } from '../schema/address.schema';

// One address belongs to one city
export const addressesRelations = relations(
  addressesTable,
  ({ one, many }) => ({
    country: one(citiesTable, {
      fields: [addressesTable.cityId],
      references: [citiesTable.id],
    }),
  }),
);
