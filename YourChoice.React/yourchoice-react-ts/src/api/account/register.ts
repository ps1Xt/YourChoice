import { baseUrl } from "../baseUrl";
import { UserForRegister } from "./models/UserForRegister";


export const register = async (data: UserForRegister): Promise<boolean> => {

    const response = await fetch(baseUrl + 'account/register/', {
        method: 'post',
        headers: {
            'Content-Type': 'application/json',
            "Accept":'application/json'
        },
        body: JSON.stringify(data)
    });
    

    if (!response.ok) {
        throw Error(response.statusText);
      }

    return true;


}