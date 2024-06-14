"use client";
import "./admin-professions.page.css"

import { useEffect, useState } from "react";
import Button from "antd/es/button/button";

import {
  ProfessionWithSlugRequest,
  createProfessionWithSlug,
  deleteProfession,
  getAllProfessionsWithSlug,
  updateProfessionWithSlug,
} from "../shared/api/professions";
import { Mode } from "../shared/ui/Mode";
import { Loading } from "../shared/ui/Loading";

import { Professions } from "./ui/Professions";
import { CreateUpdateProfession } from "./ui/CreateUpdateProfession";

export default function ProfessionsPage() {
  const [professions, setProfessions] = useState<ProfessionWithSlug[]>([]);
  const [loading, setLoading] = useState(true);
  const [mode, setMode] = useState<Mode>(Mode.Create);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const [values, setValues] = useState<ProfessionWithSlug>({
    name: "",
    description: "",
    slug: "",
  } as ProfessionWithSlug);

  const defaultValues = {
    name: "",
    description: "",
    slug: "",
  } as ProfessionWithSlug;

  useEffect(() => {
    const getProfessions = async () => {
      const professions = await getAllProfessionsWithSlug();
      setLoading(false);
      setProfessions(professions);
    };

    getProfessions();
  }, []);

  const handleCreateProfession = async (request: ProfessionWithSlugRequest) => {
    await createProfessionWithSlug(request);
    await closeModal();

    const professions = await getAllProfessionsWithSlug();
    setProfessions(professions);
  };

  const handleUpdateProfession = async (
    id: string,
    professionRequest: ProfessionWithSlugRequest
  ) => {

    await updateProfessionWithSlug(id, professionRequest);
    await closeModal();

    const professions = await getAllProfessionsWithSlug();
    setProfessions(professions);
  };

  const handleDeleteProfession = async (id: string) => {
    await deleteProfession(id);
    await closeModal();

    const professions = await getAllProfessionsWithSlug();
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

  const openEditModal = async (profession: ProfessionWithSlug) => {
    setMode(Mode.Edit);
    setValues(profession);
    setIsModalOpen(true);
  };

  return (
    <div>
      <Button
        type="primary"
        className="ad-professions__button"
        size="large"
        onClick={openModal}
      >
        Добавить профессию
      </Button>
      {loading ? (
        <Loading></Loading>
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
