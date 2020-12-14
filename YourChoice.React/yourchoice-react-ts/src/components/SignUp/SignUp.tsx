import React, { useState } from 'react';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import { Link, useHistory } from 'react-router-dom';
import Grid from '@material-ui/core/Grid';
import Box from '@material-ui/core/Box';
import CreateIcon from '@material-ui/icons/Create';
import Typography from '@material-ui/core/Typography';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import { ThemeProvider } from '@material-ui/core/styles';
import { useForm } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import * as yup from "yup";
import { FormControl } from '@material-ui/core';
import { Scrollbars } from 'react-custom-scrollbars';
import { register as fetchRegister } from '../../api/account/register';
import { UserForRegister } from '../../api/account/models/UserForRegister';
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

type Inputs = {
    userName: string;
    password: string;
    confirmPassword: string;
};
const schema = yup.object().shape({

    userName: yup.string().required('Please enter your username')
        .min(4, "Username is too short")
        .max(50, "Username is too big"),

    password: yup.string().required('Please enter your password')
        .min(6, "Password must be more than 6 characters")
        .max(100, "Password must be less than 100 characters"),

    confirmPassword: yup.string().oneOf([yup.ref('password')], "Passwords must match")


})



export default function SignUp() {
    const classes = useStyles();
    const history = useHistory();
    const { register, handleSubmit, errors } = useForm<Inputs>({
        resolver: yupResolver(schema)
    });
    const onSubmit = async (data: UserForRegister) => {
        console.log(data)
        let result = await fetchRegister(data)
        if (result)
            history.push('/SignIn')
    }
    return (
        <div style={{ position: 'absolute', top: '0px', bottom: '0px', right: '0px', left: '0px', marginTop: '64px' }}>
            <Scrollbars style={{ width: '100%', height: '100%' }}>
                <Container component="main" maxWidth="xs">
                    <CssBaseline />
                    <div className={classes.paper}>
                        <Avatar className={classes.avatar}>
                            <CreateIcon />
                        </Avatar>
                        <Typography component="h1" variant="h5">
                            Sign Up
                </Typography>
                        <form noValidate onSubmit={handleSubmit(onSubmit)}>
                            <TextField
                                variant="outlined"
                                margin="normal"
                                required
                                fullWidth
                                id="userName"
                                label="Username"
                                name="userName"
                                autoComplete="userName"
                                autoFocus
                                color="primary"
                                inputRef={register}
                            />
                            {errors.userName && <span style={{ color: 'red' }}>{errors.userName.message}</span>}

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
                            {errors.password && <span style={{ color: 'red' }}>{errors.password.message}</span>}
                            <TextField
                                variant="outlined"
                                margin="normal"
                                required
                                fullWidth
                                name="confirmPassword"
                                label="Confirm password"
                                type="password"
                                id="confirmPassword"
                                autoComplete="current-password"
                                color="primary"
                                inputRef={register}
                            />
                            {errors.confirmPassword && <span style={{ color: 'red' }}>{errors.confirmPassword.message}</span>}
                            <Button
                                type="submit"
                                fullWidth
                                variant="contained"
                                color="primary"
                                className={classes.submit}
                            >
                                Sign Up
                    </Button>
                            <Grid container>
                                <Grid item>
                                    <Link href="" to="/SignIn" color="secondary">
                                        <Typography color="primary" >
                                            Have an account? Sign In
                                </Typography>

                                    </Link>
                                </Grid>
                            </Grid>
                            <Typography>

                            </Typography>
                        </form>
                    </div>
                    <Box mt={8}>
                    </Box>
                </Container>
            </Scrollbars>
        </div>
    );
}