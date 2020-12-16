import { GetToken } from "../../helpers/JwtService";
import { baseUrl } from "../baseUrl";

export const addToFavorites = (postId: number) => {
    return fetch(baseUrl + `favorites`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            "Authorization": GetToken(),
        },
        body: JSON.stringify({
            id: postId
        })

    });
}

export default addToFavorites;