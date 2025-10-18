import { InferInsertModel, InferSelectModel } from 'drizzle-orm';
import { countriesTable } from 'src/database/drizzle/schema';
import { z } from 'zod';

export const countrySchema = z.object({
  id: z.coerce.number().int(),
  name: z.string().trim(),
});

export type CountryModel = InferSelectModel<typeof countriesTable>;
export type NewCountryModel = InferInsertModel<typeof countriesTable>;
