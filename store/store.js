import { configureStore } from '@reduxjs/toolkit'
import gatewayTokenReducer from './gatewayTokenSlice'
import grandCodeReducer from './grandCodeSlice'

export const store = configureStore({
  reducer: {
    gatewayToken: gatewayTokenReducer,
    grandCode: grandCodeReducer
  },
})