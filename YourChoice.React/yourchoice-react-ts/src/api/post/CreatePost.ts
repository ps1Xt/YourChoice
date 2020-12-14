import { GetToken } from '../../services/JwtService';
import { baseUrl } from '../baseUrl';
import { PostForCreate } from './Models/PostForCreate';



export const createPost = (data: PostForCreate) => {
    console.log(data);
    const formData = new FormData();
    formData.append("description", <string>(data.description));
    formData.append("size", data.size.toString());
    for (let i = 0; i <= data.size; i++) {

        formData.append(`image${i}`, <File>data.postParts[i].file, <string>data.postParts[i].title)
    }
    fetch(baseUrl + "post", {
        method: 'post',
        headers: {
            Authorization: GetToken(),

        },
        body: formData
    })

}



