import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import Notification from './Notification'
import { Grid } from '@material-ui/core';
import UserMenu from './UserMenu';
import CombinedStore from '../../store/CombinedStore';
import { LoginButtons } from './LoginButtons';

export default function HeaderButtons() {
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const dispatch = useDispatch()
    const isAuthenticated = useSelector<CombinedStore, boolean>(
        (s) => s.auth.isAuthenticated
    );

    const handleClick = (event: React.MouseEvent<HTMLElement>) => {
        let target = event.currentTarget
        setAnchorEl(target);

    };
    if (isAuthenticated) {
        return (
            <div>
                <Grid container alignItems='center' spacing={2}>
                    <Grid item>
                        <Notification/>

                    </Grid>
                    <Grid item>
                        <UserMenu/>
                    </Grid>

                </Grid>
            </div>
        );
    }
    else{
        
        return (<LoginButtons/>)
    }

}