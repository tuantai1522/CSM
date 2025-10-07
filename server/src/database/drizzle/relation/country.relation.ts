import { relations } from 'drizzle-orm';
import { citiesTable } from '../schema/city.schema';
import { countriesTable } from '../schema/country.schema';

export const countriesRelations = relations(countriesTable, ({ many }) => ({
  cities: many(citiesTable),
}));
