import { z } from "zod";
import { signInFormSchema } from "./schema";

export type SignInForm = z.infer<typeof signInFormSchema>;
