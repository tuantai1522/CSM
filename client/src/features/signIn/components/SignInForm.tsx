import google from "../../../assets/images/google.png";
import icon from "../../../assets/icons/icon-144x144.png";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Form, FormControl, FormField, FormItem } from "../../shared/Form";

import { FormInput } from "../../shared/FormInput";
import { PasswordInput } from "../../shared/PasswordInput";

import type { SignInForm } from "../type";
import { signInFormSchema } from "../schema";

export function SignInForm() {
  const form = useForm<SignInForm>({
    resolver: zodResolver(signInFormSchema),
    defaultValues: {
      email: "",
      passWord: "",
    },
  });

  const handleSubmit = form.handleSubmit((data) => {
    console.log(data);
  });

  return (
    <>
      <div className="bg-gray-50 dark:bg-gray-800">
        <div className="flex min-h-[80vh] flex-col justify-center py-12 sm:px-6 lg:px-8">
          <div className="flex flex-col items-center text-center sm:mx-auto sm:w-full sm:max-w-md">
            <img src={icon} className="w-16 h-16 my-4" />
            <h1 className="text-3xl font-extrabold text-gray-900 dark:text-white">
              Welcome to CSM project
            </h1>
          </div>
          <div className="mt-8 sm:mx-auto sm:w-full sm:max-w-md">
            <div className="bg-white dark:bg-gray-700 px-4 pb-4 pt-8 sm:rounded-lg sm:px-10 sm:pb-6 sm:shadow">
              <Form {...form}>
                <form onSubmit={handleSubmit} className="space-y-6">
                  <FormField
                    control={form.control}
                    name="email"
                    render={({ field }) => (
                      <FormItem>
                        <FormControl>
                          <FormInput {...field} label="Email" requiredMark />
                        </FormControl>
                      </FormItem>
                    )}
                  />

                  <FormField
                    control={form.control}
                    name="passWord"
                    render={({ field }) => (
                      <FormItem>
                        <FormControl>
                          <PasswordInput
                            {...field}
                            label="Password"
                            requiredMark
                          />
                        </FormControl>
                      </FormItem>
                    )}
                  />

                  <div className="flex items-center justify-between">
                    <div className="flex items-center">
                      <input
                        id="remember_me"
                        name="remember_me"
                        type="checkbox"
                        className="h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500 dark:text-white dark:border-gray-600 dark:focus:ring-indigo-400 disabled:cursor-wait disabled:opacity-50"
                      />
                      <label className="ml-2 block text-sm text-gray-900 dark:text-white">
                        Remember me
                      </label>
                    </div>
                    <div className="text-sm">
                      <a
                        className="font-medium text-indigo-400 hover:text-indigo-500"
                        href=""
                      >
                        Forgot your password?
                      </a>
                    </div>
                  </div>
                  <div>
                    <button
                      disabled={
                        form.formState.isSubmitting || !form.formState.isValid
                      }
                      type="submit"
                      className="group relative flex w-full justify-center rounded-md border border-transparent bg-blue-500 px-4 py-2 text-sm font-medium text-white hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 dark:bg-indigo-700 dark:border-transparent dark:hover:bg-indigo-600 dark:focus:ring-indigo-400 dark:focus:ring-offset-2 disabled:opacity-50"
                    >
                      Sign In
                    </button>
                  </div>
                </form>
              </Form>

              <div className="mt-6">
                <div className="relative">
                  <div className="absolute inset-0 flex items-center">
                    <div className="w-full border-t border-gray-300"></div>
                  </div>
                  <div className="relative flex justify-center text-sm">
                    <span className="bg-white dark:bg-gray-700 px-2 text-gray-500 dark:text-white">
                      Or continue with
                    </span>
                  </div>
                </div>
                <div className="mt-6">
                  <button className="flex items-center justify-center w-full h-[42px] rounded-md gap-3 text-black text-base font-medium transition hover:bg-gray-100 hover:shadow-md hover:text-blue-600 cursor">
                    <img src={google} className="w-6 h-6" />
                    <span className="uppercase">Đăng nhập bằng Google</span>
                  </button>
                </div>
              </div>
              <div className="m-auto mt-6 w-fit md:mt-8 flex items-center space-x-2 dark:text-gray-400">
                <span>Don't have an account?</span>
                <span>
                  <a
                    className="font-semibold text-indigo-600 dark:text-indigo-100"
                    href="/register"
                  >
                    Create Account
                  </a>
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
