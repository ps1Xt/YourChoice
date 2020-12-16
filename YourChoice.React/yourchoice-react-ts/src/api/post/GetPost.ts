import { GetToken } from "../../helpers/JwtService";
import { baseUrl } from "../baseUrl";
import { PostForView } from "./Models/PostForView";

export const GetPost = async (id: number): Promise<PostForView> => {
    let response = await fetch(baseUrl + `post/${id}`, {
        method: 'get',
        headers: {
            "Accept": "application/json",
            Authorization: GetToken()

        }
    })
    if(!response.ok){
        if(response.status == 404 )
            throw Error("Post not found")

         else throw Error("Something went wrong")
    }

    let result = await response.json()

    return result

}