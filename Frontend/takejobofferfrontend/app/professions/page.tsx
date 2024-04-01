"use client";

import Button from "antd/es/button/button";
import Title from "antd/es/typography/Title";
import { Professions } from "../components/Professions";
import {
  ProfessionRequest,
  createProfession,
  deleteProfession,
  getAllProfessions,
  updateProfession,
} from "../services/professions";
import { useEffect, useState } from "react";
import { CreateUpdateProfession } from "../components/CreateUpdateProfession";
import { Mode } from "../components/Mode";

export default function ProfessionsPage() {
  const [professions, setProfessions] = useState<Profession[]>([]);
  const [loading, setLoading] = useState(true);
  const [mode, setMode] = useState<Mode>(Mode.Create);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const [values, setValues] = useState<Profession>({
    name: "",
    description: "",
  } as Profession);

  const defaultValues = {
    name: "",
    description: "",
  } as Profession;

  useEffect(() => {
    const getProfessions = async () => {
      const professions = await getAllProfessions();
      setLoading(false);
      setProfessions(professions);
    };

    getProfessions();
  }, []);

  const handleCreateProfession = async (request: ProfessionRequest) => {
    await createProfession(request);
    closeModal();

    const professions = await getAllProfessions();
    setProfessions(professions);
  };

  const handleUpdateProfession = async (
    id: string,
    professionRequest: ProfessionRequest
  ) => {
    await updateProfession(id, professionRequest);
    closeModal();

    const professions = await getAllProfessions();
    setProfessions(professions);
  };

  const handleDeleteProfession = async (id: string) => {
    await deleteProfession(id);
    closeModal();

    const professions = await getAllProfessions();
    setProfessions(professions);
  };

  const openModal = async () => {
    setMode(Mode.Create);
    setIsModalOpen(true);
  };

  const closeModal = async () => {
    setValues(defaultValues);
    setIsModalOpen(false);
  };

  const openEditModal = async (profession: Profession) => {
    setMode(Mode.Edit);
    setValues(profession);
    setIsModalOpen(true);
  };

  return (
    <div>
      <Button
        type="primary"
        style={{ marginTop: "30px" }}
        size="large"
        onClick={openModal}
      >
        Добавить профессию
      </Button>
      {loading ? (
        <Title>Loading...</Title>
      ) : (
        <Professions
          professions={professions}
          handleOpen={openEditModal}
          handleDelete={handleDeleteProfession}
        />
      )}
      <CreateUpdateProfession
        mode={mode}
        values={values}
        isModalOpen={isModalOpen}
        handleCreate={handleCreateProfession}
        handleUpdate={handleUpdateProfession}
        handleCancel={closeModal}
      />
    </div>
  );
}
