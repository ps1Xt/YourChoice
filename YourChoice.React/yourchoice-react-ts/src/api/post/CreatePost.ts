import { GetToken } from '../../helpers/JwtService';
import { baseUrl } from '../baseUrl';
import { PostForCreate } from './Models/PostForCreate';



export const createPost = async (data: PostForCreate) => {
    const formData = new FormData();
    try {
        formData.append("description", <string>(data.description));
    }
    catch {
        throw Error("Data is not valid")
    }
    let i;
    try {
        for ( i = 0; i <= data.size; i++) {

            formData.append(`image${i}`, <File>data.postParts[i].file, <string>data.postParts[i].title)
        }
    }
    catch{
        if(i == 0){
             throw new Error("please append logo")
        }
        else{
            throw Error("No file or it is not valid at " + i +" part")
        }
    }
    
    
    let response = await fetch(baseUrl + "post", {
        method: 'post',
        headers: {
            Authorization: GetToken(),

        },
        body: formData
    })

    if (!response.ok)
        throw Error("Failed to load post")
        
    let result = await response.json()
    
    return result;
    

}



