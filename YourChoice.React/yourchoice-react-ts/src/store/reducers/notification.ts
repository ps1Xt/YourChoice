import { on, reducer } from "ts-action";
import { newMessage, readMessages, setNewMessages } from "../actions/_notification";

export interface numberOfNewMessages {
    number: number
}

const initialState: numberOfNewMessages = {
    number: 0
};

export default reducer(
    initialState,
    on(newMessage, (state) => ({
        ...state,
        number: state.number + 1
    })),
    on(readMessages, (state) => ({
        ...state,
        number: 0
    })),
    on(setNewMessages, (state, { payload }) => ({
        ...state,
        number: payload?.number
    })
    ))


