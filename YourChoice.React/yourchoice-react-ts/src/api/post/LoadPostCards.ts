import { GetToken } from "../../services/JwtService";
import { baseUrl } from "../baseUrl";
import { MainPageRequest } from "./Models/MainPageRequest";

export const loadPostCards = async (mainPageRequest: MainPageRequest, section: string) => {
    let response = await fetch(baseUrl + `post/mainpage/${section}`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            "Authorization": GetToken(),
        },
        body: JSON.stringify(mainPageRequest)

    });
    console.log(response)
    if (!response.ok)
        if (response.status == 401)
            throw Error("Please Login to see this data");
        else
            throw Error("Something went wrong");

    return await response.json()
}