import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Menu, { MenuProps } from '@material-ui/core/Menu';
import MenuItem from '@material-ui/core/MenuItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import DraftsIcon from '@material-ui/icons/Drafts';
import ArrowDropDownOutlinedIcon from '@material-ui/icons/ArrowDropDownOutlined';
import AddOutlinedIcon from '@material-ui/icons/AddOutlined';
import ExitToAppOutlinedIcon from '@material-ui/icons/ExitToAppOutlined';
import { useDispatch } from 'react-redux';
import { logoutUser } from '../../store/actions/_auth';
import AccountCircleIcon from '@material-ui/icons/AccountCircle';
import { useHistory } from "react-router-dom";
import GridOnIcon from '@material-ui/icons/GridOn';
const StyledMenu = withStyles({
    paper: {
        border: '1px solid #d3d4d5',
    },
})((props: MenuProps) => (
    <Menu
        elevation={0}
        getContentAnchorEl={null}
        anchorOrigin={{
            vertical: 'bottom',
            horizontal: 'center',
        }}
        transformOrigin={{
            vertical: 'top',
            horizontal: 'center',
        }}
        {...props}
    />
));

const StyledMenuItem = withStyles((theme) => ({
    root: {
        '&:focus': {
            backgroundColor: theme.palette.primary.main,
            '& .MuiListItemIcon-root, & .MuiListItemText-primary': {
                color: theme.palette.common.white,
            },
        },
    },
}))(MenuItem);

export default function UserMenu() {
    const [anchorEl, setAnchorEl] = React.useState<null | HTMLElement>(null);
    const dispatch = useDispatch()
    const history = useHistory()
    const handleClick = (event: React.MouseEvent<HTMLElement>) => {
        let target = event.currentTarget
        setAnchorEl(target);

    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    const logoutHandler = () => {
        dispatch(logoutUser())
    }

    const createPostHandler = () =>{
        history.push("/createpost")
        handleClose();
    }
    const gridHandler = () =>{
        history.push("/grid")
        handleClose();
    }

    return (
        <div>

            <Button
                aria-controls="customized-menu"
                aria-haspopup="true"
                variant="outlined"
                color="primary"
                onClick={handleClick}
                startIcon={<AccountCircleIcon />}
                endIcon={<ArrowDropDownOutlinedIcon />}
            >
            </Button>
            <StyledMenu

                id="customized-menu"
                anchorEl={anchorEl}
                keepMounted
                open={Boolean(anchorEl)}
                onClose={handleClose}
            >
                <StyledMenuItem onClick={createPostHandler}>
                    <ListItemIcon>
                        <AddOutlinedIcon fontSize="small" />
                    </ListItemIcon>
                    <ListItemText primary="Create Post" />
                </StyledMenuItem>
                <StyledMenuItem onClick={gridHandler}>
                    <ListItemIcon>
                        <GridOnIcon fontSize="small" />
                    </ListItemIcon>
                    <ListItemText primary="Grid" />
                </StyledMenuItem>
                <StyledMenuItem onClick={logoutHandler} >
                    <ListItemIcon>
                        <ExitToAppOutlinedIcon fontSize="small" />
                    </ListItemIcon>
                    <ListItemText primary="Logout" />
                </StyledMenuItem>
            </StyledMenu>

        </div>
    );
}