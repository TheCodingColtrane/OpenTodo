import React from 'react';
import { Modal, Box, Typography } from '@mui/material';
import { Category } from '../../../../types';
import { useForm } from 'react-hook-form';
import { backend, categoriesEndpoints } from '../../../../backend';
import { CategoriesModalProps, NewCategoryResponse } from '../types';
import { showMessage } from '../../../../utils';

export const CategoriesModal: React.FC<CategoriesModalProps> = ({ open, handleClose, handleCategories}) => {
  const {
    register,
    handleSubmit,
  } = useForm<Category>();
  
  const onSubmit = handleSubmit(async (data) => {
    if(data.name === "" || window.location.href.split("/")[4] === ""){
      showMessage("Campos inválidos.")
    }
    const response = await backend.post(categoriesEndpoints.base, {name: data.name, board: {code: window.location.href.split("/")[4]}})
    if(response?.ok) {
      const category = await response.json() as NewCategoryResponse
      handleCategories({code: category.code, name: data.name, createdAt: new Date(), boardCode: "", value: data.value  })
      alert('Nova categoria cadastrada com sucesso.')
    }
  })   

  return (
    <Modal open={open} onClose={handleClose}>
      <Box
        sx={{
          position: 'absolute',
          top: '50%',
          left: '50%',
          transform: 'translate(-50%, -50%)',
          width: 800,
          bgcolor: 'black',
          border: '2px solid #000',
          borderRadius: '10px',
          boxShadow: 24,
          p: 4,
        }}
      >
    <Typography id="modal-modal-title" variant="h6" component="h2">
        Criar nova lista
        </Typography>
        <Typography sx={{ mt: 2 }}>
          <form onSubmit={onSubmit}>
            <div style={{padding: '10px', display: 'flex', flexDirection: 'column', alignItems: 'center'  } }>
            {/* <label htmlFor="category">Nome da lista</label> */}
            <input type="text" minLength={3} maxLength={50} style={{width: '60%', padding: '7px'}} placeholder='Nome da lista'  {...register("name")} onChange={(e) => e}/>
            <button type="submit" style={{marginTop: "10px", width: '60%'}}>Criar</button>
            </div>
          </form>
        </Typography>
      </Box>
    </Modal>
  );
};

export default CategoriesModal;
