import { createSlice } from "@reduxjs/toolkit"

const gatewayTokenSlice = createSlice({
    name: 'gatewayToken',
    initialState: {
        data: '',
    },
    reducers: {
        setToken(state, action) {
            state.data = action.payload
        },
    }
})

export const {setToken} = gatewayTokenSlice.actions

export default gatewayTokenSlice.reducer