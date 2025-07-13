import google from "../../assets/images/google.png";
import icon from "../../assets/icons/icon-144x144.png";

export function SignInForm() {
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
              <form className="space-y-6">
                <div>
                  <label className="block text-sm font-medium text-gray-700 dark:text-white">
                    Email address
                  </label>
                  <div className="mt-1">
                    <input
                      id="email"
                      type="text"
                      data-testid="username"
                      className="block w-full appearance-none rounded-md border border-gray-300 px-3 py-2 placeholder-gray-400 shadow-sm focus:border-indigo-500 focus:outline-none focus:ring-indigo-500 dark:bg-gray-800 dark:border-gray-600 dark:text-white dark:placeholder-gray-300 dark:focus:border-indigo-400 dark:focus:ring-indigo-400 sm:text-sm"
                      value=""
                    />
                  </div>
                </div>
                <div>
                  <label className="block text-sm font-medium text-gray-700 dark:text-white">
                    Password
                  </label>
                  <div className="mt-1">
                    <input
                      id="password"
                      name="password"
                      type="password"
                      data-testid="password"
                      className="block w-full appearance-none rounded-md border border-gray-300 px-3 py-2 placeholder-gray-400 shadow-sm focus:border-indigo-500 focus:outline-none focus:ring-indigo-500 dark:bg-gray-800 dark:border-gray-600 dark:text-white dark:placeholder-gray-300 dark:focus:border-indigo-400 dark:focus:ring-indigo-400 sm:text-sm"
                      value=""
                    />
                  </div>
                </div>
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
                    data-testid="login"
                    type="submit"
                    className="group relative flex w-full justify-center rounded-md border border-transparent bg-indigo-600 px-4 py-2 text-sm font-medium text-white hover:bg-indigo-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 dark:bg-indigo-700 dark:border-transparent dark:hover:bg-indigo-600 dark:focus:ring-indigo-400 dark:focus:ring-offset-2 disabled:cursor-wait disabled:opacity-50"
                  >
                    <span className="absolute inset-y-0 left-0 flex items-center pl-3">
                      <svg
                        className="h-5 w-5 text-indigo-500 group-hover:text-indigo-400"
                        xmlns="http://www.w3.org/2000/svg"
                        viewBox="0 0 20 20"
                        fill="currentColor"
                        aria-hidden="true"
                      >
                        <path
                          fill-rule="evenodd"
                          d="M5 9V7a5 5 0 0110 0v2a2 2 0 012 2v5a2 2 0 01-2 2H5a2 2 0 01-2-2v-5a2 2 0 012-2zm8-2v2H7V7a3 3 0 016 0z"
                          clip-rule="evenodd"
                        ></path>
                      </svg>
                    </span>
                    Sign In
                  </button>
                </div>
              </form>
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
                  <button className="flex items-center justify-center w-full h-[42px] rounded-md gap-3 text-black text-base font-medium transition hover:bg-gray-100 hover:shadow-md hover:text-blue-600">
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
