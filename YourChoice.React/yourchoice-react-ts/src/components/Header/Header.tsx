import React from 'react';
import { Button } from '@material-ui/core';
import { Container, Toolbar, AppBar} from '@material-ui/core';
import { Grid } from '@material-ui/core';
import { makeStyles, createStyles, Theme } from '@material-ui/core/styles';
import { Link } from 'react-router-dom';
import { SiteLogo } from './SiteLogo';
import { useSelector } from 'react-redux';
import CombinedStore from '../../store/CombinedStore';
import HeaderButtons from './HeaderButtons';
const useStyles = makeStyles((theme: Theme) =>
    createStyles({

        noOutline: {
            outline: 'none',
            textDecoration: 'none'
        },
        red: {
            color: '#ff0000',
            fontWeight: 700

        },
        white: {
            color: '#ffffff',
            fontWeight: 700
        }


    }),
);


export function Header() {
    const isAuthenticated = useSelector<CombinedStore, boolean>(
        (s) => s.auth.isAuthenticated
    );
    function Buttons(isAuthenticated: boolean) {
        if (isAuthenticated) {

            return (
                <>
                    <HeaderButtons></HeaderButtons>
                </>
            )
        }
        else {
            return (
                <>
                    <Grid item >
                        <Link style={{ textDecoration: 'none' }} href="" to="/SignIn">
                            <Button variant="outlined" color="primary" className={classes.noOutline}  >Sign In</Button>

                        </Link>
                    </Grid>
                    <Grid item >
                        <Link style={{ textDecoration: 'none' }} href="" to="/SignUp">
                            <Button color="secondary" variant="outlined" className={classes.noOutline}>Sign Up</Button>
                        </Link>
                    </Grid>
                </>
            )
        }
    }
    const noOutline = {
        outline: 'none'
    }
    const classes = useStyles();


    return (

        <AppBar position="static" style={{ background: "#222222", height: '64px' }}>
            <Container>
                <Toolbar >
                    <Grid container direction="row"
                        justify="flex-start"
                        alignItems="center"
                        spacing={3} >
                        <Grid item  >
                            <Link style={{ textDecoration: 'none' }} href="" to="/"  >
                                <SiteLogo />
                            </Link>
                        </Grid>
                    </Grid>
                    <Grid container justify="flex-end" spacing={3}>
                        {Buttons(isAuthenticated)}
                    </Grid>
                </Toolbar>
            </Container>
        </AppBar>
    );
}
