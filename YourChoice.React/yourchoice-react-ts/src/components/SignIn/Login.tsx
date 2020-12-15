import React, { useEffect, useState } from 'react';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import { Link } from 'react-router-dom';
import Grid from '@material-ui/core/Grid';
import Box from '@material-ui/core/Box';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import { ThemeProvider } from '@material-ui/core/styles';
import HeaderTheme from '../../Themes/NavTheme'
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from "yup";
import { FormControl } from '@material-ui/core';
import { Scrollbars } from 'react-custom-scrollbars';
import CircularProgress from '@material-ui/core/CircularProgress';
import { useHistory } from "react-router-dom";
import { useDispatch, useSelector } from 'react-redux';
import CombinedStore from '../../store/CombinedStore';
import { loginUser } from '../../store/actions/_auth'
import { UserForLogin } from '../../api/account/models/UserForLogin';
import { LoadingBox } from '../Common/LoadingBox';
const useStyles = makeStyles((theme) => ({
    paper: {
        marginTop: theme.spacing(8),
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
    },
    avatar: {
        margin: theme.spacing(1),
        backgroundColor: '#0cdf2f',
    },
    form: {
        width: '100%', // Fix IE 11 issue.
        marginTop: theme.spacing(1),
    },
    submit: {
        margin: theme.spacing(3, 0, 2),
    },
}));


const schema = yup.object().shape({
    username: yup.string().required(),
    password: yup.string().required(),
});

function ViewLoading(view: boolean) {
    if (view) {
        return (
            <Grid container justify="center" style={{ marginTop: "25px" }} >
                <CircularProgress color="secondary" />
            </Grid>
        )
    }
}



export function SignIn() {
    const classes = useStyles();
    const history = useHistory();
    const [loading, setLoading] = useState(false);
    const dispatch = useDispatch();

    const isAuthenticated = useSelector<CombinedStore, boolean>(
        (s) => s.auth.isAuthenticated
    );

    const isAuthenticating = useSelector<CombinedStore, boolean>(
        (s) => s.auth.isAuthenticating
    );

    const authErrors = useSelector<CombinedStore, boolean>(
        (s) => s.auth.errors
    );
    

    const { register, handleSubmit, errors } = useForm<UserForLogin>({
        resolver: yupResolver(schema)
    });
    const OnSubmitHandler = (data: UserForLogin) => {
        dispatch(loginUser(data));
        setLoading(true);
        if (isAuthenticating) {
            return;
        }
        
    }
    useEffect(() => {
        if (isAuthenticated) {
            history.push('/');
        }
        setLoading(false)

    }, [isAuthenticated, history,authErrors])

    return (
        <div style={{ position: 'absolute', top: '0px', bottom: '0px', right: '0px', left: '0px', marginTop: '64px' }}>
            <Scrollbars style={{ width: '100%', height: '100%' }}>
                <Container component="main" maxWidth="xs">
                    <CssBaseline />
                    <div className={classes.paper}>
                        <Avatar className={classes.avatar}>
                            <LockOutlinedIcon />
                        </Avatar>
                        <Typography component="h1" variant="h5">
                            Sign in
                        </Typography>
                        <form noValidate onSubmit={handleSubmit(OnSubmitHandler)}>
                            <TextField
                                variant="outlined"
                                margin="normal"
                                required
                                fullWidth
                                id="username"
                                label="Username"
                                name="username"
                                autoComplete="username"
                                autoFocus
                                color="primary"
                                inputRef={register}
                            />
                            <TextField
                                variant="outlined"
                                margin="normal"
                                required
                                fullWidth
                                name="password"
                                label="Password"
                                type="password"
                                id="password"
                                autoComplete="current-password"
                                color="primary"
                                inputRef={register}
                            />
                            <Button
                                type="submit"
                                fullWidth
                                variant="contained"
                                color="primary"
                                className={classes.submit}
                            >
                                Sign In
                            </Button>
                            <Grid container>
                                <Grid item>
                                    <Link href="" to="/SignUp" color="secondary">
                                        <Typography color="primary" >
                                            Don't have an account? Sign Up
                                        </Typography>

                                    </Link>
                                </Grid>
                            </Grid>
                            {loading &&
                             <div style={{textAlign:'center',marginTop:'20px'}}>
                                 <CircularProgress />
                            </div>}
                            {authErrors && !loading &&
                                <Typography style={{textAlign:'center'}} variant="h6" color="secondary">Username or password entered incorrectly</Typography>
                            }
                        </form>
                    </div>

                </Container>
            </Scrollbars>
        </div>
    );
}