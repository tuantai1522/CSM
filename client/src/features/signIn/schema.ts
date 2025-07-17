import { z } from "zod";

export const signInFormSchema = z.object({
  email: z.email(),
  passWord: z.string().min(8),
});
