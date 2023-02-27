import { createSlice } from "@reduxjs/toolkit"

const grandCodeSlice = createSlice({
    name: 'grandCode',
    initialState: {
        data: '',
    },
    reducers: {
        setGrantCode(state, action) {
            state.data = action.payload
        },
    }
})

export const {setGrantCode} = grandCodeSlice.actions

export default grandCodeSlice.reducer