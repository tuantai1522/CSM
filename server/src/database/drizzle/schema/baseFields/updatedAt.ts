import { bigint } from 'drizzle-orm/pg-core';

export const updatedAt = {
  updatedAt: bigint('updated_at', { mode: 'number' }).notNull(),
};
