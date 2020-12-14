import { baseUrl } from "../baseUrl";
import { BearerToken } from "./models/BearerToken";
import { UserForLogin } from "./models/UserForLogin";


export const login = async (form: UserForLogin) => {
  const response = await fetch(baseUrl + 'account/login/', {
    method: 'post',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(form)
  });

  if (!response.ok) {
    throw Error(response.statusText);
  }

  const data: BearerToken = await response.json();
  return data;
}
