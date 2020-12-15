import { action, payload } from 'ts-action';
import { login } from '../../api/account/login';
import { ThunkAction } from 'redux-thunk';
import CombinedStore from '../CombinedStore';
import { AnyAction } from 'redux';
import { UserForLogin } from '../../api/account/models/UserForLogin';


export type AsyncAction = ThunkAction<
    void | Promise<void>,
    CombinedStore,
    void,
    AnyAction
>;

export const loginUserSuccess =
    action('LOGIN_USER_SUCCESS', payload<{ token: string }>());

export const loginUserFailure = action('LOGIN_USER_FAILURE');

export const loginUserRequest = action('LOGIN_USER_REQUEST');

export const fetchCurrentUserFailure = action('FETCH_CURRENT_USER_FAILURE');

export const logoutUserSuccess = action('LOGOUT_USER');

export const loginUser = (form: UserForLogin): AsyncAction => async (dispatch) => {
    let token: string;
    try {
        dispatch(loginUserRequest());
        const  data = await login(form);
        const { accessToken } = data;
        token = accessToken;
        localStorage.setItem('token', token);
        dispatch(loginUserSuccess({ token }));
    }
    catch {
        dispatch(loginUserFailure());
    }

}
export const logoutUser = (): AsyncAction => async (dispatch) => {
    localStorage.removeItem('token');
    dispatch(logoutUserSuccess());
}

export const fetchCurrentUser = (): AsyncAction => async (dispatch) => {
    const token = localStorage.getItem('token');
    if (token != null) {
        dispatch(loginUserSuccess({ token }));
    } else {
        dispatch(fetchCurrentUserFailure());
    }
}

