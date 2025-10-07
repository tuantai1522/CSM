import { relations } from 'drizzle-orm';
import { citiesTable } from '../schema/city.schema';
import { countriesTable } from '../schema/country.schema';
import { addressesTable } from '../schema/address.schema';

export const citiesRelations = relations(citiesTable, ({ one, many }) => ({
  country: one(countriesTable, {
    fields: [citiesTable.countryId],
    references: [countriesTable.id],
  }),
  address: many(addressesTable),
}));
