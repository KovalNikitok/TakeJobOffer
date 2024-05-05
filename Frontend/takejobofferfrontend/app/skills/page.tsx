"use client";

import Button from "antd/es/button/button";
import Title from "antd/es/typography/Title";
import { Skills } from "../components/Skills"
import {
  SkillRequest,
  getAllSkills,
  getSkillById,
  createSkill,
  deleteSkill,
  updateSkill,
} from "../services/skills";
import { useEffect, useState } from "react";
import { CreateUpdateSkill } from "../components/CreateUpdateSkill";
import { Mode } from "../components/Mode";

export default function SkillsPage() {
  const [skills, setSkills] = useState<Skill[]>([]);
  const [loading, setLoading] = useState(true);
  const [mode, setMode] = useState<Mode>(Mode.Create);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const [values, setValues] = useState<Skill>({
    name: "",
  } as Skill);

  const defaultValues = {
    name: "",
  } as Skill;

  useEffect(() => {
    const getSkills = async () => {
      const skills = await getAllSkills();
      setLoading(false);
      setSkills(skills);
    };

    getSkills();
  }, []);

  const handleCreateSkill = async (request: SkillRequest) => {
    await createSkill(request);
    await closeModal();

    const curSkills = await getAllSkills();
    setSkills(curSkills);
  };

  const handleUpdateSkill = async (id: string, skillRequest: SkillRequest) => {
    await updateSkill(id, skillRequest);
    await closeModal();

    const curSkills = await getAllSkills();
    setSkills(curSkills);
  };

  const handleDeleteSkill = async (id: string) => {
    await deleteSkill(id);
    await closeModal();

    const curSkills = await getAllSkills();
    setSkills(curSkills);
  };

  const openModal = async () => {
    setMode(Mode.Create);
    setIsModalOpen(true);
  };

  const closeModal = async () => {
    setValues(defaultValues);
    setIsModalOpen(false);
  };

  const openEditModal = async (skill: Skill) => {
    setMode(Mode.Edit);
    setValues(skill);
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
        Добавить навык
      </Button>
      {loading ? (
        <Title>Loading...</Title>
      ) : (
        <Skills
          skills={skills}
          handleOpen={openEditModal}
          handleDelete={handleDeleteSkill}
        />
      )}
      <CreateUpdateSkill
        mode={mode}
        values={values}
        isModalOpen={isModalOpen}
        handleCreate={handleCreateSkill}
        handleUpdate={handleUpdateSkill}
        handleCancel={closeModal}
      />
    </div>
  );
}
