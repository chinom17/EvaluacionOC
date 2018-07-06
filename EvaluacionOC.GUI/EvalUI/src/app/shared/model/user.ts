import { Genero } from './genero';
export interface User {
  id: number;
  email: string;
  nombreUsuario: string;
  password: string;
  status: boolean;
  statusDesc?: string;
  generoDesc?: string;
  generoId: number;
  genero?: Genero;
  fechaCreacion?: Date;
  strFechaCreacion?: string;
}
