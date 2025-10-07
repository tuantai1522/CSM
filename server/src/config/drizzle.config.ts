import * as dotenv from 'dotenv';
import path from 'path';

import { defineConfig } from 'drizzle-kit';
dotenv.config({ path: path.resolve(__dirname, '../.env') });
export default defineConfig({
  schema: '../database/drizzle/schema/index.ts',
  out: '../database/drizzle/migrations',
  dialect: 'postgresql',
  dbCredentials: {
    url: process.env.DATABASE_URL!,
  },
});
