import { Input } from "antd";
import { SkillRequest } from "../services/skills";
import { useEffect, useState } from "react";
import Modal from "antd/es/modal/Modal";
import { Mode } from "./Mode"

interface Props {
  mode: Mode;
  values: Skill;
  isModalOpen: boolean;
  handleCancel: () => void;
  handleCreate: (request: SkillRequest) => void;
  handleUpdate: (id: string, request: SkillRequest) => void;
}

export const CreateUpdateSkill = ({
  mode,
  values,
  isModalOpen,
  handleCancel,
  handleCreate,
  handleUpdate,
}: Props) => {
  const [name, setName] = useState<string>("");

  useEffect(() => {
    setName(values.name);
  }, [values]);

  const handleOnOk = async () => {
    const skillRequest = { name };

    mode == Mode.Create
      ? handleCreate(skillRequest)
      : handleUpdate(values.id, skillRequest);
  };

  return (
    <Modal
      title={
        mode === Mode.Create ? "Добавить навык" : "Редактировать навык"
      }
      open={isModalOpen}
      onOk={handleOnOk}
      onCancel={handleCancel}
      cancelText={"Отмена"}
    >
      <div className="skill__modal">
        <Input
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder="Навык"
        />
      </div>
    </Modal>
  );
};
