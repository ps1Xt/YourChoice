import { on, reducer } from "ts-action";
import { setUpdate } from "../actions/_createPost";

export interface CreatePostStore {
    update: number
}

const initialState: CreatePostStore = {
    update: 0
};

export default reducer(
    initialState,
    on(setUpdate, (state)=>({
        ...state,
        update: state.update + 1
    }))
    
)

