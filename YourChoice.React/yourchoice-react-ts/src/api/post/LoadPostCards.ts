import { GetToken } from "../../helpers/JwtService";
import { baseUrl } from "../baseUrl";
import { PaginatedResult } from "../grid/models/PaginatedResult";
import { MainPageRequest } from "./Models/MainPageRequest";
import { PostCard } from "./Models/PostCard";

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

    let result: PaginatedResult<PostCard> = await response.json()

    return result;
}