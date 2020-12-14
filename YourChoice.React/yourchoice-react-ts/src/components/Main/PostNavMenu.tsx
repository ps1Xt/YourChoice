import React, { useEffect, useState } from 'react';
import Paper from '@material-ui/core/Paper';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import StarsIcon from '@material-ui/icons/Stars';
import FavoriteIcon from '@material-ui/icons/Favorite';
import Toolbar from '@material-ui/core/Toolbar';
import Search from './Search';
import HomeIcon from '@material-ui/icons/Home';
import AccountBoxIcon from '@material-ui/icons/AccountBox';
import { Grid } from '@material-ui/core';
import { MainPageRequest } from '../../api/post/Models/MainPageRequest';


export interface pageRequestState {
    pageRequest: [MainPageRequest, React.Dispatch<React.SetStateAction<MainPageRequest>>]
}

export default function PostNavMenu(props: pageRequestState) {

    const [pageRequest, setPageRequest] = props.pageRequest;
    const [sectionValue, setSectionValue] = useState(0);
    const [orderValue, setOrderValue] = useState(0)
    useEffect(() => {
    }, [])

    const sectionChange = (event: any, newValue: number) => {

        if (newValue == 0)
            pageRequest.section = "Home"
        else if (newValue == 1)
            pageRequest.section = "Subscriptions"
        else if (newValue == 2)
            pageRequest.section = "Favorites"
        else if (newValue == 3)
            pageRequest.section = "MyPosts"

        setPageRequest({
            ...pageRequest,
        })
        setSectionValue(newValue);
    };
    const orderChange = (event: any, newValue: number) => {

        if (newValue == 0)
            pageRequest.columnNameForSorting = 'avgRating'
        else if (newValue == 1)
            pageRequest.columnNameForSorting = 'date'

        setPageRequest({
            ...pageRequest,
        })
        setOrderValue(newValue);
    };
    return (
        <Paper >
            <Grid container justify='space-between' alignItems='center'>

                <Grid item>
                    <Tabs
                        value={sectionValue}
                        onChange={sectionChange}
                        variant="fullWidth"
                        indicatorColor="secondary"
                        textColor="secondary"
                        aria-label="icon label tabs example"
                    >
                        <Tab icon={<HomeIcon />} label="Home" />
                        <Tab icon={<StarsIcon />} label="Subscriptions" />
                        <Tab icon={<FavoriteIcon />} label="Favorites" />
                        <Tab icon={<AccountBoxIcon />} label="My posts" />



                    </Tabs>
                </Grid>
                <Grid item>
                    <Toolbar>
                        <Search pageRequest={[pageRequest, setPageRequest]}></Search>
                    </Toolbar>

                </Grid>
                <Grid item xs={12}>
                    <Tabs
                        value={orderValue}
                        onChange={orderChange}
                        variant="fullWidth"
                        indicatorColor="primary"
                        textColor="primary"
                        aria-label="icon label tabs example"
                    >
                        <Tab label="Popular" />
                        <Tab label="Newest" />



                    </Tabs>
                </Grid>
            </Grid>

        </Paper>
    );
}
