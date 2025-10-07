import { bigint } from 'drizzle-orm/pg-core';

export const createdAt = {
  createdAt: bigint('created_at', { mode: 'number' }).notNull(),
};
