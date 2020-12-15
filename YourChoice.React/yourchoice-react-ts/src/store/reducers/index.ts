import {combineReducers} from 'redux';
import auth from './auth';
import createPost from './createPost'
import notification from './notification'
export default combineReducers({
 auth : auth,
 createPost : createPost,
 notification : notification
});
